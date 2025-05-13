﻿using EjemploPersonasData.Commons;
using EjemploPersonasData.DALs;
using EjemploPersonasData.DALs.MSSDALs;
using EjemploPersonasData.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EjemploPersonasData.Services;

public class PersonasService
{
    readonly private PersonasMSSDAL _personasDao;

    private readonly IConfiguration _configuracion;
    private readonly ITransaction<SqlTransaction> _transaction;

    public PersonasService(PersonasMSSDAL personasDao, IConfiguration configuracion, ITransaction<SqlTransaction> transaction)
    {
        _personasDao = personasDao;
        _configuracion = configuracion;
        _transaction = transaction;
    }

    async public Task<List<PersonaModel?>?> GetAll()
    {
        return await _personasDao?.GetAll();
    }

    async public Task<PersonaModel?> GetById(int id)
    {
        return await _personasDao.GetByKey(id);
    }

    async public Task CrearNuevo(PersonaModel objeto)
    {
        await _personasDao.Insert(objeto);
    }

    async public Task Actualizar(PersonaModel objeto)
    {
        await _personasDao.Update(objeto);
    }

    async public Task Eliminar(int id)
    {
        try
        {
            await _transaction.BeginTransaction();

            var objeto = await _personasDao.GetByKey(id, _transaction);
            if (objeto != null)
            {
                await _personasDao.Delete(id, _transaction);
            }

            await _transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await _transaction.RollbackAsync();
            throw ex;
        }
    }

    [Transaction(Propagation = "Required", RollbackFor = "Exception")]
    async public Task Eliminar2(int id)
    {
        try
        {
            var objeto = await _personasDao.GetByKey(id, _transaction);
            if (objeto != null)
            {
                await _personasDao.Delete(id, _transaction);
            }

            await _transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await _transaction.RollbackAsync();
            throw ex;
        }
    }
}
