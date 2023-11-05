using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeDatabase {
    [Serializable]
    public class ResumeModel {
        public string Fio, Age, FamilyStatus, Address, Email, IsInfo, IsLanguage, IsCommunicate;
        public ResumeModel() {
            Fio = Age = FamilyStatus = Address = Email = IsInfo = IsLanguage = IsCommunicate = string.Empty;
        }
    }
}
