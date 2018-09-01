using System.Collections.Generic;

namespace Negocio.DTO
{
    public class ProductoXCategorias
    {
        public ProductosDTO Producto { get; set; }
        public List<CategoriaCheckbox> Categorias { get; set; }

        public ProductoXCategorias()
        {
            Producto = new ProductosDTO();
            Categorias = new List<CategoriaCheckbox>();
        }
    }
    public class CategoriaCheckbox
    {
        public CategoriasDTO Categoria { get; set; }
        public bool Selected { get; set; }
    }
}
