using System.Collections.Generic;
using System.Linq;


namespace FSTEC.Objects
{
    public class Update
    {
        public int Id { get; private set; }
        public string Param { get; private set; }
        public string Old { get; set; }
        public string Recent { get; set; }
        public Update(int id, string param, string old, string recent)
        {
            Id = id;
            Param = param;
            Old = old;
            Recent = recent;
        }

        private static List<Update> GetUpdatesBetweenNewLast(Danger lastDanger, Danger newDanger)
        {
            List<Update> updates = new List<Update>();
            if (lastDanger.Name != newDanger.Name)
                updates.Add(new Update(lastDanger.Id, "Наименование УБИ", lastDanger.Name.ToString(), newDanger.Name.ToString()));
            if (lastDanger.Description != newDanger.Description)
                updates.Add(new Update(lastDanger.Id, "Описание", lastDanger.Description.ToString(), newDanger.Description.ToString()));
            if (lastDanger.Source != newDanger.Source)
                updates.Add(new Update(lastDanger.Id, "Источник угрозы (характеристика и потенциал нарушителя)", lastDanger.Source.ToString(), newDanger.Source.ToString()));
            if (lastDanger.Target != newDanger.Target)
                updates.Add(new Update(lastDanger.Id, "Объект воздействия", lastDanger.Target.ToString(), newDanger.Target.ToString()));
            if (lastDanger.PrivacyBreach != newDanger.PrivacyBreach)
                updates.Add(new Update(lastDanger.Id, "Нарушение конфиденциальности", FromBoolToStringConverter(lastDanger.PrivacyBreach), FromBoolToStringConverter(newDanger.PrivacyBreach)));
            if (lastDanger.IntegrityBreach != newDanger.IntegrityBreach)
                updates.Add(new Update(lastDanger.Id, "Нарушение целостности", FromBoolToStringConverter(lastDanger.IntegrityBreach), FromBoolToStringConverter(newDanger.IntegrityBreach)));
            if (lastDanger.AccessBreach != newDanger.AccessBreach)
                updates.Add(new Update(lastDanger.Id, "Нарушение доступности", FromBoolToStringConverter(lastDanger.AccessBreach), FromBoolToStringConverter(newDanger.AccessBreach)));
            return updates;
        }

        public static List<Update> GetUpdates(List<Danger> lastDangers, List<Danger> newDangers)
        {
            List<Update> updates = new List<Update>();
            foreach (var danger in lastDangers)
            {
                Danger tempDanger = newDangers.Where(el => el.Id == danger.Id).Select(el => el).First();
                updates = updates.Union(GetUpdatesBetweenNewLast(danger, tempDanger)).ToList();
            }
            return updates;
        }

        private static string FromBoolToStringConverter(bool b)
        {
            if (b == true)
            {
                return "Да";
            }
            else
            {
                return "Нет";
            }
        }

    }
}
