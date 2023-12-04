using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject {
    public class UserModel {
        private static UserModel? example;
        public string? Login, Password;
        public static UserModel Example {
            get {
                if (example == null) example = new UserModel();
                return example;
            }
        }
        private UserModel() => Login = Password = null;
    }
    public class ImagesModel {
        public string? Name, Date, Author, Path;
        public List<KeyValue> Ratings;
        public ImagesModel() {
            Name = Date = Author = Path = null;
            Ratings = new List<KeyValue>();
        }
    }
}
