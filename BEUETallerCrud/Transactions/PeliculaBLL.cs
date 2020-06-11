using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEUETallerCrud.Transactions
{
    public class PeliculaBLL
    {
        public static void Create(Pelicula alumno)
        {
            using (DBPLATAFORMAPELICULASEntities db = new DBPLATAFORMAPELICULASEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Pelicula.Add(alumno);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }

            }
        }
        //Actualizar un elemento de la base de datos
        //Debe pertenecer el contexto de la BD como la variable de la clase Alumno
        //a una misma instancia de DBEntities

        public static Pelicula Get(int? id)
        {
            DBPLATAFORMAPELICULASEntities db = new DBPLATAFORMAPELICULASEntities();

            return db.Pelicula.Find(id);
        }


        public static void Update(Pelicula pelicula, int? id)
        {
            using (DBPLATAFORMAPELICULASEntities db = new DBPLATAFORMAPELICULASEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //db.Entry(alumno).State = System.Data.Entity.EntityState.Modified;
                        pelicula.id_pelicula = id.Value;
                        db.Pelicula.Attach(pelicula);
                        db.Entry(pelicula).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }

            }


        }

        //eliminar un registro de BD
        public static void Delete(int? id)
        {
            using (DBPLATAFORMAPELICULASEntities db = new DBPLATAFORMAPELICULASEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        Pelicula pelicula = db.Pelicula.Find(id);
                        db.Entry(pelicula).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }

            }



        }

        public static List<Pelicula> List()
        {
            DBPLATAFORMAPELICULASEntities db = new DBPLATAFORMAPELICULASEntities();//Instancia del contexto
            return db.Pelicula.ToList();
            //Los metodos del entity framework se denomina Linq, y la evaluacion de condiciones lambda
        }


        private static List<Pelicula> GetPeliculas(string criterio)
        {
            //starswith inicio de palabra
            //contains que contiene en cualquier lado de la cadena
            DBPLATAFORMAPELICULASEntities db = new DBPLATAFORMAPELICULASEntities();//Instancia del contexto
            return db.Pelicula.Where(x => x.nombre_pelicula.ToLower().StartsWith(criterio)).ToList();
        }

        /*public static List<Alumno> GetAlumno(int id)
        {
            DBEntities db = new DBEntities();//Instancia del contexto
            return db.Alumno.Where(x => x.idalumno == id).ToList();
        }*/

        private static Pelicula GetPelicula(string nombre)
        {
            DBPLATAFORMAPELICULASEntities db = new DBPLATAFORMAPELICULASEntities();//Instancia del contexto
            return db.Pelicula.FirstOrDefault(x => x.nombre_pelicula == nombre);
        }
    }
}
