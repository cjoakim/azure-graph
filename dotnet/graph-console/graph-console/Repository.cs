using System.Runtime.Serialization;
using System;
using System.Globalization;

namespace graph_console {

    [DataContract(Name="repo")]
    public class Repository : IComparable {

        [DataMember(Name="name")]
        public string Name { get; set; }

        [DataMember(Name = "private")]
        public string IsPrivate { get; set; }

        [DataMember(Name="description")]
        public string Description { get; set; }
        
        [DataMember(Name="html_url")]
        public Uri GitHubHomeUrl { get; set; }
        
        [DataMember(Name="homepage")]
        public Uri Homepage { get; set; }
        
        [DataMember(Name="watchers")]
        public int Watchers { get; set; }
        
        [DataMember(Name="pushed_at")]
        private string JsonDate { get; set; }
        
        [IgnoreDataMember]
        public DateTime LastPush
        {
            get
            {
                return DateTime.ParseExact(JsonDate, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
            }
        }

        public string visibility() {

            if (IsPrivate.ToLower() == "true") {
                return "private";
            }
            return "public";
        }
        public override String ToString() {

            return Name + " | " + visibility() + " | " + GitHubHomeUrl; // + " -> " + Description;
        }

        int IComparable.CompareTo(object obj) {

            Repository another = (Repository)obj;
            return String.Compare(this.Name, another.Name);

        }
    }
}