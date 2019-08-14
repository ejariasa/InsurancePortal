using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Repository.Data
{
    public class MapperHelper<T, U> : MapperHelper where T : class where U : class
    {
        /// <summary>
        /// Updates the specified entity to update.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        /// <param name="entityUpdated">The entity updated.</param>
        public T setValues(T entityToUpdate, U entityUpdated)
        {
            try
            {
                //Gets the properties of each entity
                PropertyInfo[] propertyInfoT = entityToUpdate.GetType().GetProperties();
                PropertyInfo[] propertyInfoU = entityUpdated.GetType().GetProperties();

                //Loops throug every property in the entity to update
                foreach (PropertyInfo infoT in propertyInfoT)
                {
                    string nameT = infoT.Name;
                    PropertyInfo infoU = propertyInfoU.Where(x => x.Name.ToUpper() == nameT.ToUpper()).FirstOrDefault();

                    if (infoU != null)
                    {
                        var valueT = infoT.GetValue(entityToUpdate, null);
                        var valueU = infoU.GetValue(entityUpdated, null);

                        if (!object.Equals(valueT, valueU) && valueU != null)
                        {
                            infoT.SetValue(entityToUpdate, valueU, null);
                        }
                    }
                }

                return entityToUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    public class MapperHelper
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="oEntity">The o entity.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <returns></returns>
        public object getValue(object oEntity, string paramName)
        {
            var type = oEntity.GetType();
            var prop = type.GetProperty(paramName);
            return prop == null ? null : prop.GetValue(oEntity);
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <param name="oEntity">The o entity.</param>
        /// <returns></returns>
        public string getName(object oEntity)
        {
            PropertyInfo[] propertyInfo = oEntity.GetType().GetProperties();
            return propertyInfo[0].Name;
        }

        /// <summary>
        /// Gets the object according to the type name
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <param name="root">The root.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public object getObject(string typeName, object root)
        {
            try
            {
                PropertyInfo[] propertyInfo = root.GetType().GetProperties();

                foreach (PropertyInfo info in propertyInfo)
                {
                    if (info.Name.Equals(typeName, StringComparison.InvariantCultureIgnoreCase) ||
                       info.PropertyType.Name.Equals(typeName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return info.GetValue(root, null);
                    }
                }

                //If it gets here, check generics
                foreach (PropertyInfo info in propertyInfo)
                {
                    if (info.Name.Equals("entity", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return info.GetValue(root, null);
                    }
                }

                return null;
            }
            catch (Exception ex)            {
               
                throw new Exception(ex.Message);
            }
        }
    }
}
