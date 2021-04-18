using System.Collections.Generic;


namespace FSTEC.Objects
{
    public class Danger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
        public bool PrivacyBreach { get; set; }
        public bool IntegrityBreach { get; set; }
        public bool AccessBreach { get; set; }

        public Danger (Dictionary<string, string> dangers)
        {
            Id = int.Parse(dangers["Идентификатор УБИ"]);
            Name = dangers["Наименование УБИ"];
            Description = dangers["Описание"];
            Source = dangers["Источник угрозы (характеристика и потенциал нарушителя)"];
            Target = dangers["Объект воздействия"];
            if (dangers["Нарушение конфиденциальности"] == "1")
            {
                PrivacyBreach = true;
            }
            else
            {
                PrivacyBreach = false;
            }
            if (dangers["Нарушение целостности"] == "1")
            {
                IntegrityBreach = true;
            }
            else
            {
                IntegrityBreach = false;
            }
            if (dangers["Нарушение доступности"] == "1")
            {
                AccessBreach = true;
            }
            else
            {
                AccessBreach = false;
            }
        }

        public Danger()
        {

        }

        public override string ToString()
        {
            return $"Id: {Id}\nName: {Name}\nDescription: {Description}\nSource: {Source}\nTarget: {Target}\nPrivacy: {PrivacyBreach}";
        }

        public override bool Equals(object obj)
        {
            Danger inputDanger = obj as Danger;
            if (this.Id == inputDanger.Id &&
                this.Name == inputDanger.Name &&
                this.Description == inputDanger.Description &&
                this.Source == inputDanger.Source &&
                this.Target == inputDanger.Target &&
                this.PrivacyBreach == inputDanger.PrivacyBreach &&
                this.IntegrityBreach == inputDanger.IntegrityBreach &&
                this.AccessBreach == inputDanger.AccessBreach
                )
                return true;
            return false;
        }
    }
}
