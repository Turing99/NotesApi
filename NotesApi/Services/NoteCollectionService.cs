using NotesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NotesApi.Services
{
    public class NoteCollectionService : INoteCollectionService
    {
        private static List<Notes> _notes = new List<Notes> { new Notes { Id = new System.Guid("2c0332ca-6f03-41a3-b331-aebc7f932f9a"), CategoryId = "1", OwnerId = new System.Guid("b081f808-7482-4bac-9733-8ffee3e4e1d9"), Title = "First Note", Description = "First Note Description" },
        new Notes { Id = Guid.NewGuid(), CategoryId = "1", OwnerId = new System.Guid(), Title = "Second Note", Description = "Second Note Description" },
        new Notes { Id = Guid.NewGuid(), CategoryId = "3", OwnerId = new System.Guid(), Title = "Third Note", Description = "Third Note Description" },
        new Notes { Id = Guid.NewGuid(), CategoryId = "2", OwnerId = new System.Guid(), Title = "Fourth Note", Description = "Fourth Note Description" },
        new Notes { Id = Guid.NewGuid(), CategoryId = "1", OwnerId = new System.Guid(), Title = "Fifth Note", Description = "Fifth Note Description" }
        };

        public bool Create(Notes model)
        {
            _notes.Add(model);
            bool isAdded = _notes.Contains(model);
            return isAdded;
        }

        public bool Delete(Guid id)
        {
            var index = _notes.FindIndex(note => note.Id.Equals(id));
            if (index == -1)
            {
                return false;
            }

            _notes.RemoveAt(index);

            return true;
        }

        public Notes Get(Guid id)
        {
            return _notes.FirstOrDefault(note => note.Id.Equals(id));
        }

        public List<Notes> GetAll()
        {
            return _notes;
        }

        public List<Notes> GetNotesByOwnerId(Guid ownerId)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid id, Notes model)
        {
            var index = _notes.FindIndex(note => note.Id.Equals(id));

            if (index == -1)
            {
                return false;
            }
            model.Id = _notes[index].Id;
            _notes[index] = model;

            return true;
        }
    }
}
