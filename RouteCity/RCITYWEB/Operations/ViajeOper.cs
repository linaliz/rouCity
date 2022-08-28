using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using DBRouCity;

namespace RCITYWEB.Controllers
{
    public class ViajeOper
    {
        private RouCityEntities db = new RouCityEntities();
        public int Save(Viaje viaje)
        {
            int id = 0;

            try
            {
                //if ()
                //{
                  
                  // db.Entry(nuevoAlmacen).CurrentValues.SetValues(almacen);
                //}
                //else
                //{
                    //db.Almacen.Add(almacen);
                //}

                //db.SaveChanges();
                //id = almacen.idAlmacen;
            }
            catch (DbEntityValidationException e)
            {
                id = -2;
            }
            catch (Exception ex)
            {
                id = -1;
            }
            return id;
        }

    }
}
