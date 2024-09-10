using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_repositorios;
using lib_repositorios.Implementaciones;
using mst_prueba_unitarias.Nucleo;

namespace mst_prueba_unitarias.Aplicaciones
{
    [TestClass]
    public class ProductosUnitTest
    {
        private ProductosAplicacion? iAplicacion = null;
        private Conexion? conexion = null;
        private Productos? entidad = null;
        private List<Productos>? lista = null;

        public ProductosUnitTest()
        {
            conexion = new Conexion();
            conexion!.StringConnection = ConfiguracionHelper.ObtenerValor("ConectionString");
            iAplicacion = new ProductosAplicacion(new ProductosRepositorio(conexion));
        }

        [TestMethod]
        public void Ejecutar()
        {
            Guardar();
            Listar();
            Buscar();
            Modificar();
            Borrar();
        }

        private void Listar()
        {
            lista = iAplicacion!.Listar();
            Assert.IsTrue(lista.Count > 0);
        }

        public void Buscar()
        {
            lista = iAplicacion!.Buscar(entidad!, "CODIGO");
            Assert.IsTrue(lista.Count > 0);
        }

        public void Guardar()
        {
            entidad = EntidadesHelper.ObtenerProductos();
            entidad = iAplicacion!.Guardar(entidad!);
            Assert.IsTrue(entidad.Id != 0);
        }

        public void Modificar()
        {
            entidad!.Nombre = entidad.Nombre + " " + DateTime.Now.ToString();
            entidad = iAplicacion!.Modificar(entidad!);

            lista = iAplicacion!.Buscar(entidad!, "CODIGO");
            Assert.IsTrue(lista.Count > 0);
        }

        public void Borrar()
        {
            entidad = iAplicacion!.Borrar(entidad!);

            lista = iAplicacion!.Buscar(entidad!, "CODIGO");
            Assert.IsTrue(lista.Count == 0);
        }
    }
}