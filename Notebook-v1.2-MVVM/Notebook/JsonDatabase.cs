using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook {
    [Serializable]
    public class PersonsCollection {
        public ObservableCollection<PersonModel> Persons { get; set; }
    }
    class JsonDatabase {
        static JsonDatabase pointer;
        public static JsonDatabase Pointer {
            get {
                if (pointer == null) pointer = new JsonDatabase();
                return pointer;
            }
        }
        public void Serialize(ObservableCollection<PersonsVM> collection, string path) {
            var pers = new PersonsCollection {
                Persons = new ObservableCollection<PersonModel>(collection.Select(vm => vm.Person.Clone()))
            };
            var json = JsonConvert.SerializeObject(pers, Formatting.Indented);
            File.WriteAllText(path, json);
        }
        public ObservableCollection<PersonsVM> Deserialize(string path) {
            if (File.Exists(path)) {
                var json = File.ReadAllText(path);
                var pers = JsonConvert.DeserializeObject<PersonsCollection>(json);
                return new ObservableCollection<PersonsVM>(
                    pers.Persons.Select(persM => new PersonsVM { Person = persM })
                );
            }
            return new ObservableCollection<PersonsVM>();
        }
    }
}
