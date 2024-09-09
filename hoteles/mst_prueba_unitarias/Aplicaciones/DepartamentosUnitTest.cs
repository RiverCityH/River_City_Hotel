using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_repositorios;
using lib_repositorios.Implementaciones;
using mst_prueba_unitarias.Nucleo;

namespace mst_prueba_unitarias.Aplicaciones
{
    [TestClass]
    public class DepartamentosUnitTest
    {
        private DepartamentosAplicacion? iAplicacion = null;
        private Conexion? conexion = null;
        private Departamentos? entidad = null;
        private List<Departamentos>? lista = null;

        public DepartamentosUnitTest()
        {
            conexion = new Conexion();
            conexion!.StringConnection = ConfiguracionHelper.ObtenerValor("ConectionString");
            iAplicacion = new DepartamentosAplicacion(new DepartamentosRepositorio(conexion));
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
            lista = iAplicacion!.Buscar(entidad!, "NOMBRE");
            Assert.IsTrue(lista.Count > 0);
        }

        public void Guardar()
        {
            entidad = EntidadesHelper.ObtenerDepartamentos();
            entidad = iAplicacion!.Guardar(entidad!);
            Assert.IsTrue(entidad.Id != 0);
        }

        public void Modificar()
        {
            entidad!.Nombre = entidad.Nombre + " " + DateTime.Now.ToString();
            entidad = iAplicacion!.Modificar(entidad!);

            lista = iAplicacion!.Buscar(entidad!, "NOMBRE");
            Assert.IsTrue(lista.Count > 0);
        }

        public void Borrar()
        {
            entidad = iAplicacion!.Borrar(entidad!);

            lista = iAplicacion!.Buscar(entidad!, "NOMBRE");
            Assert.IsTrue(lista.Count == 0);
        }
    }
}