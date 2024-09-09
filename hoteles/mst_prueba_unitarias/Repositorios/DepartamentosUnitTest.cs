using lib_entidades_dominio;
using lib_repositorios;
using lib_repositorios.Implementaciones;
using mst_prueba_unitarias.Nucleo;

namespace mst_prueba_unitarias.Repositorios
{
    [TestClass]
    public class DepartamentosUnitTest
    {
        private DepartamentosRepositorio? iRepositorio = null;
        private Conexion? conexion = null;
        private Departamentos? entidad = null;
        private List<Departamentos>? lista = null;

        public DepartamentosUnitTest()
        {
            conexion = new Conexion();
            conexion!.StringConnection = ConfiguracionHelper.ObtenerValor("ConectionString");
            iRepositorio = new DepartamentosRepositorio(conexion);
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
            lista = iRepositorio!.Listar();
            Assert.IsTrue(lista.Count > 0);
        }

        public void Buscar()
        {
            lista = iRepositorio!.Buscar(x => x.Id == entidad!.Id);
            Assert.IsTrue(lista.Count > 0);
        }

        public void Guardar()
        {
            entidad = EntidadesHelper.ObtenerDepartamentos();
            entidad = iRepositorio!.Guardar(entidad!);
            Assert.IsTrue(entidad.Id != 0);
        }

        public void Modificar()
        {
            entidad!.Nombre = entidad.Nombre + " " + DateTime.Now.ToString();
            entidad = iRepositorio!.Modificar(entidad!);

            lista = iRepositorio!.Buscar(x => x.Id == entidad.Id);
            Assert.IsTrue(lista.Count > 0);
        }

        public void Borrar()
        {
            entidad = iRepositorio!.Borrar(entidad!);

            lista = iRepositorio!.Buscar(x => x.Id == entidad!.Id);
            Assert.IsTrue(lista.Count == 0);
        }
    }
}