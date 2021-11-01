using NotesApi.Models;
using System;
using System.Collections.Generic;

namespace NotesApi.Services
{
    public interface INoteCollectionService : ICollectionService<Notes>
    {
        List<Notes> GetNotesByOwnerId(Guid ownerId);
    }
}
