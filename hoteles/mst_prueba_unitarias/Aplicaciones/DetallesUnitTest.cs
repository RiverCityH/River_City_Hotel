using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_repositorios;
using lib_repositorios.Implementaciones;
using mst_prueba_unitarias.Nucleo;

namespace mst_prueba_unitarias.Aplicaciones
{
    [TestClass]
    public class DetallesUnitTest
    {
        private DetallesAplicacion? iAplicacion = null;
        private Conexion? conexion = null;
        private Detalles? entidad = null;
        private List<Detalles>? lista = null;

        public DetallesUnitTest()
        {
            conexion = new Conexion();
            conexion!.StringConnection = ConfiguracionHelper.ObtenerValor("ConectionString");
            iAplicacion = new DetallesAplicacion(new DetallesRepositorio(conexion));
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
            lista = iAplicacion!.Buscar(entidad!, "ID");
            Assert.IsTrue(lista.Count > 0);
        }

        public void Guardar()
        {
            entidad = EntidadesHelper.ObtenerDetalles();
            entidad = iAplicacion!.Guardar(entidad!);
            Assert.IsTrue(entidad.Id != 0);
        }

        public void Modificar()
        {
            entidad!.Cantidad = entidad.Cantidad + 1;
            entidad = iAplicacion!.Modificar(entidad!);

            lista = iAplicacion!.Buscar(entidad!, "ID");
            Assert.IsTrue(lista.Count > 0);
        }

        public void Borrar()
        {
            entidad = iAplicacion!.Borrar(entidad!);

            lista = iAplicacion!.Buscar(entidad!, "ID");
            Assert.IsTrue(lista.Count == 0);
        }
    }
}