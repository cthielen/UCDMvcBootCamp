﻿using FluentNHibernate.Mapping;
using UCDMvcBootCamp.Core.Domain;

namespace UCDMvcBootCamp.Core.Mappings
{
    public class SessionMap : ClassMap<Session>
    {
        public SessionMap()
        {
            Table("Sessions");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Title);
            Map(x => x.Abstract);
            
            References(x => x.Conference);
            References(x => x.Speaker).Cascade.SaveUpdate();
        }
    }
}
