using PrandoWebServices.Data.Entities;
using PrandoWebServices.Repositories;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using CoreDbContext = Microsoft.EntityFrameworkCore.DbContext;


namespace PrandoWebServices.UOW
{
    public interface IUnitofWork
    {
        void BeginTransaction();
        Task<int> Commit();

        CoreDbContext Context { get; }

        IRepository<Attachment> AttachmentRepository { get; }
        IRepository<AutoMaker> AutoMakerRepository { get; }
        IRepository<AutoModel> AutoModelRepository { get; }
        IRepository<Owner> OwnerRepository { get; }
        IRepository<VehicleOwner> VehicleOwnerRepository { get; }
        IRepository<Vehicle> VehicleRepository { get; }

        void Dispose(bool disposing);
        void Rollback();
        Task<int> SaveChanges();
        Task<int> SaveChanges(CancellationToken cancellationToken);
        void Save();
    }
}
