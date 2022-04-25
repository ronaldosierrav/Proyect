using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using ConexionBD.ViewsModels;
using System.Threading.Tasks;

namespace ConexionBD.Data
{
    public class SQLiteH
    {
        SQLiteAsyncConnection db;
        public SQLiteH(string dbpath)
        {
            db = new SQLiteAsyncConnection(dbpath);
            db.CreateTableAsync<persona>().Wait();
        }

        public Task<int> SavePersonaAsync(persona Persona)
        {
            if (Persona.Id != 0)
            {
                return db.UpdateAsync(Persona);
            }
            else
            {
                return db.InsertAsync(Persona);
            }
        }

        public Task<int> DeletePersonaAsync(persona Persona)
        {
            return db.DeleteAsync(Persona);
        }
        /// <summary>
        /// Recuperacion de personas
        /// </summary>
        /// <returns>Id Persona</returns>
        public Task<List<persona>> GetpersonasAsync()
        {
            return db.Table<persona>().ToListAsync();
        }
        /// <summary>
        /// Recupera Personas Por Identificacion
        /// </summary>
        /// <param name="Id">Id Persona que busca</param>
        /// <returns></returns>
        public Task<persona>GetPersonaByIdAsync(int Id)
        {
            return db.Table<persona>().Where(x => x.Id == Id).FirstOrDefaultAsync();
        }
        
    }
}
