/************************************************************************

*创建标记：啄木鸟

*创建时间：2016/8/22 15:53:35

*创建人：曾南华 

*版本号： V1.0.0.0

*描述：DDD
*************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.DTOEntity.DTO;
using Web.EFEntity;

namespace Web.DTOEntity.ConfigRegister
{
    internal class MenuToTree:IRegisterMapper
    {
        public Dictionary<string, string> CreateUrl(Menu s)
        {
            var temp = new Dictionary<string, string>();
            temp.Add("url", s.Area + "/" + s.ControllerName + "/" + s.ActionName);
            temp.Add("Area", s.Area);
            temp.Add("ControllerName", s.ControllerName);
            temp.Add("ActionName", s.ActionName);
            temp.Add("OrderNo", s.OrderNo.ToString());
            return temp;
        }

        public void Execute()
        {
            AutoMapper.Mapper.CreateMap<Menu, Tree>().ForMember(c => c.id, opt => opt.MapFrom(s => s.MenuId))
                .ForMember(c=>c.text,opt=>opt.MapFrom(s=>s.MenuName))
                .ForMember(c=>c.state,opt=>opt.MapFrom(s=>"open"))
                .ForMember(c=>c.attributes,opt=>opt.MapFrom(s=>CreateUrl(s)));
        }
    }
}
