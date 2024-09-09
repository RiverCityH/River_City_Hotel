using lib_aplicaciones.Implementaciones;
using lib_entidades_dominio;
using lib_repositorios;
using lib_repositorios.Implementaciones;
using mst_prueba_unitarias.Nucleo;

namespace mst_prueba_unitarias.Aplicaciones
{
    [TestClass]
    public class EmpleadosUnitTest
    {
        private EmpleadosAplicacion? iAplicacion = null;
        private Conexion? conexion = null;
        private Empleados? entidad = null;
        private List<Empleados>? lista = null;

        public EmpleadosUnitTest()
        {
            conexion = new Conexion();
            conexion!.StringConnection = ConfiguracionHelper.ObtenerValor("ConectionString");
            iAplicacion = new EmpleadosAplicacion(new EmpleadosRepositorio(conexion));
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
            lista = iAplicacion!.Buscar(entidad!, "PERSONA");
            Assert.IsTrue(lista.Count > 0);
        }

        public void Guardar()
        {
            entidad = EntidadesHelper.ObtenerEmpleados();
            entidad = iAplicacion!.Guardar(entidad!);
            Assert.IsTrue(entidad.Id != 0);
        }

        public void Modificar()
        {
            entidad!.EPS = 2;
            entidad = iAplicacion!.Modificar(entidad!);

            lista = iAplicacion!.Buscar(entidad!, "PERSONA");
            Assert.IsTrue(lista.Count > 0);
        }

        public void Borrar()
        {
            entidad = iAplicacion!.Borrar(entidad!);

            lista = iAplicacion!.Buscar(entidad!, "PERSONA");
            Assert.IsTrue(lista.Count == 0);
        }
    }
}