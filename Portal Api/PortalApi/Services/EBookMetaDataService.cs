﻿using Portal_Api.Models;

namespace Portal_Api.Services;
public class EBookMetaDataService : IEBookMetaDataService
{
    private readonly EBookMetaDataDbContext _context;

    public EBookMetaDataService(EBookMetaDataDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public EBookMetaData AddBookMetaData(EBookMetaData eBookMetaData)
    {
        _context.EBookMetaData.Add(eBookMetaData);
        _context.SaveChanges();
        return eBookMetaData;
    }

    /// <inheritdoc/>
    public bool DeleteEBookMetaData(int bookId)
    {
        var eBookMetaData = _context.EBookMetaData.FirstOrDefault(e => e.Id == bookId);
        if (eBookMetaData != null)
        {
            _context.Remove(eBookMetaData);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    /// <inheritdoc/>
    public EBookMetaData? GetEBookMetaData(int bookId)
    {
        return _context.EBookMetaData.FirstOrDefault(e => e.Id == bookId);
    }

    /// <inheritdoc/>
    public List<EBookMetaData> GetEBookMetaDataList()
    {
        return _context.EBookMetaData.ToList();
    }
}