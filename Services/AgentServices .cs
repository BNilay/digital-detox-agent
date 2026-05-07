using ai.Models;

namespace ai.Services
{
    public class AgentService : IAgentService
    {
        public Plan GeneratePlan(User user, List<UsageRecord> usageRecords)
        {
            var lastRecord = usageRecords
                .OrderByDescending(x => x.Date)
                .FirstOrDefault();

            if (lastRecord == null)
            {
                return new Plan
                {
                    UserId = user.Id,
                    RiskLevel = "Veri yok",
                    WeeklyGoal = "Önce günlük kullanım verisi girilmelidir.",
                    DailySuggestion = "Bugünkü ekran sürenizi sisteme ekleyin.",
                    AlternativeActivity = "Kısa yürüyüş, kitap okuma veya telefonsuz zaman planı oluşturabilirsiniz."
                };
            }
            if (lastRecord.ScreenTime <= user.TargetScreenTime)
            {
                return new Plan
                {
                    UserId = user.Id,
                    RiskLevel = "Düşük",
                    WeeklyGoal = $"Mevcut ekran süren {lastRecord.ScreenTime} saat ve hedefin olan {user.TargetScreenTime} saatin altında. Bu nedenle azaltma planı yerine mevcut düzeni korumaya odaklanabilirsin.",
                    DailySuggestion = "Bugünkü kullanımın hedefinle uyumlu görünüyor. Aynı dengeyi korumaya çalış.",
                    AlternativeActivity = "Telefon kullanımını artırmadan mevcut rutini sürdürmek için kısa yürüyüş, kitap okuma veya çevrimdışı bir hobi tercih edebilirsin."
                };
            }

            int riskScore = CalculateRiskScore(user, lastRecord);
            string riskLevel = DetermineRiskLevel(riskScore);

            string weeklyGoal = GenerateWeeklyGoal(user, lastRecord, riskLevel);
            string dailySuggestion = GenerateDailySuggestion(lastRecord);
            string alternativeActivity = GenerateAlternativeActivity(lastRecord);

            return new Plan
            {
                UserId = user.Id,
                RiskLevel = riskLevel,
                WeeklyGoal = weeklyGoal,
                DailySuggestion = dailySuggestion,
                AlternativeActivity = alternativeActivity
            };
        }

        private int CalculateRiskScore(User user, UsageRecord lastRecord)
        {
            int score = 0;

            if (lastRecord.ScreenTime >= 7)
            {
                score += 3;
            }
            else if (lastRecord.ScreenTime >= 4)
            {
                score += 2;
            }
            else
            {
                score += 1;
            }

            double differenceFromTarget = lastRecord.ScreenTime - user.TargetScreenTime;

            if (differenceFromTarget >= 3)
            {
                score += 2;
            }
            else if (differenceFromTarget >= 1)
            {
                score += 1;
            }

            if (lastRecord.UsagePeriod == "Gece")
            {
                score += 1;
            }

            if (lastRecord.Reason == "Alışkanlık" || lastRecord.Reason == "Stres")
            {
                score += 1;
            }

            return score;
        }

        private string DetermineRiskLevel(int riskScore)
        {
            if (riskScore >= 5)
            {
                return "Yüksek";
            }

            if (riskScore >= 3)
            {
                return "Orta";
            }

            return "Düşük";
        }

        private string GenerateWeeklyGoal(User user, UsageRecord lastRecord, string riskLevel)
        {
            if (riskLevel == "Yüksek")
            {
                return $"Bu hafta ekran süreni {lastRecord.ScreenTime} saatten doğrudan {user.TargetScreenTime} saate indirmeye çalışmak yerine, önce 1-1.5 saat azaltmayı hedefle.";
            }

            if (riskLevel == "Orta")
            {
                return $"Bu hafta ekran süreni {lastRecord.ScreenTime} saatten {user.TargetScreenTime} saate yaklaştırmak için her gün küçük azaltmalar yap.";
            }

            return $"Ekran süren hedefe yakın görünüyor. Bu hafta mevcut düzeni koruyarak {user.TargetScreenTime} saat hedefini sürdürmeye çalış.";
        }

        private string GenerateDailySuggestion(UsageRecord lastRecord)
        {
            if (lastRecord.UsagePeriod == "Gece")
            {
                return $"{lastRecord.MostUsedApp} kullanımını özellikle gece saatlerinde sınırlandır. Yatmadan 30 dakika önce telefonu bırakmayı dene.";
            }

            if (lastRecord.UsagePeriod == "Sabah")
            {
                return $"Güne {lastRecord.MostUsedApp} ile başlamak yerine ilk 30 dakikayı telefonsuz geçirmeyi dene.";
            }

            if (lastRecord.UsagePeriod == "Akşam")
            {
                return $"Akşam saatlerinde {lastRecord.MostUsedApp} kullanımına zaman sınırı koy. Örneğin 30 dakikalık bir kullanım limiti belirle.";
            }

            return $"{lastRecord.MostUsedApp} kullanımını gün içinde belirli zaman aralıklarıyla sınırlandırmayı dene.";
        }

        private string GenerateAlternativeActivity(UsageRecord lastRecord)
        {
            if (lastRecord.Reason == "Sıkılma")
            {
                return "Sıkıldığında telefonu açmak yerine 20 dakikalık yürüyüş, kısa bir hobi veya kitap okuma deneyebilirsin.";
            }

            if (lastRecord.Reason == "Stres")
            {
                return "Stres anlarında sosyal medyaya yönelmek yerine 5 dakikalık nefes egzersizi veya kısa bir mola rutini deneyebilirsin.";
            }

            if (lastRecord.Reason == "Ders/işten kaçış")
            {
                return "Ders veya işten kaçış yaşadığında 25 dakika çalışma + 5 dakika mola şeklinde Pomodoro tekniğini deneyebilirsin.";
            }

            if (lastRecord.Reason == "Alışkanlık")
            {
                return "Telefonu refleks olarak kontrol etmemek için bildirimleri kapatabilir ve telefonu çalışma alanının dışında tutabilirsin.";
            }

            return "Telefon yerine kısa yürüyüş, esneme, kitap okuma veya çevrimdışı bir aktivite tercih edebilirsin.";
        }
    }
}