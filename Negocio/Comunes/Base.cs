using AutoMapper;
using Negocio.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Comunes
{
    public class Base
    {
        public static enums.MotorDb MotorBd;
        public static void InitializeMapper()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ConexionBD.MsSql.Log, LogDTO>().ReverseMap();
                cfg.CreateMap<ConexionBD.MsSql.Categorias, CategoriasDTO>().ReverseMap();
                cfg.CreateMap<ConexionBD.MsSql.Productos, ProductosDTO>().ReverseMap();
                cfg.CreateMap<ConexionBD.MsSql.Cateoria_Producto, Cateoria_ProductoDTO>().ReverseMap();
                cfg.CreateMap<ConexionBD.MsSql.Clientes, ClientesDTO>().ReverseMap();
                cfg.CreateMap<ConexionBD.MsSql.Pedidos, PedidosDTO>().ReverseMap();
                cfg.CreateMap<ConexionBD.MsSql.Producto_Pedido, Producto_PedidoDTO>().ReverseMap();

                //cfg.CreateMap<ConexionBD.Oracle.Categorias, CategoriasDTO>().ReverseMap();
            });
        }
    }
}
