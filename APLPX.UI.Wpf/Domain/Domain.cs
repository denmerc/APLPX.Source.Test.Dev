using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ReactiveUI;

namespace APLPX.Client.Display
{        
        public class Analytic : ReactiveUI.ReactiveObject //TODO: ReactiveValidatedEntity
        {
            [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
            public string Id { get; set; }
            public int AnalyticId { get; set; }
            public string Name { get; set; }
            
            public string Status { get; set; }
            
            public string Description { get; set; }
            public DateTime LastUpdated { get; set; }
            public string LastUserUpdated { get; set; }
            public string Comments { get; set; }
            private List<string> _Tags;
            public List<string> Tags { get { return _Tags; } set { this.RaiseAndSetIfChanged(ref _Tags, value); } }
            public List<string> FavoriteTags { get; set; }            
            public bool Shared { get; set; }
        }


        public class Filter : ReactiveObject
        {
            public int _id { get; set; }
            private Boolean _isSelected;
            public Boolean IsSelected { get { return _isSelected; } set { this.RaiseAndSetIfChanged(ref _isSelected, value); } } //TODO: should be in viewmodel or in datastore?

            private string _code;
            [BsonElement("code")]
            public string Code { get { return _code; } set { this.RaiseAndSetIfChanged(ref _code, value); } }
            private string _description;
            [BsonElement("description")]
            public string Description { get { return _description; } set { this.RaiseAndSetIfChanged(ref _description, value); } }
        }

    
}
