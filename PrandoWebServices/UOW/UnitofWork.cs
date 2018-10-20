using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using PrandoWebServices.Data.Entities;
using PrandoWebServices.Repositories;
using CoreDbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace PrandoWebServices.UOW
{
    public class UnitofWork : IUnitofWork
    {
        private bool disposed = false;

        private IDbContextTransaction dbContextTransaction;
        private IRepository<Attachment> attachmentRepository;
        private IRepository<AutoMaker> automakerRepository;
        private IRepository<AutoModel> automodelRepository;
        private IRepository<Owner> ownerRepository;
        private IRepository<VehicleOwner> vehicleOwnerRepository;
        private IRepository<Vehicle> vehicleRepository;
        public CoreDbContext Context { get; }
        public UnitofWork(CoreDbContext context)
        {
            Context = context;
        }
        public IRepository<Attachment> AttachmentRepository
        {
            get
            {
                return this.attachmentRepository ?? (this.attachmentRepository =
                    new Repository<Attachment>(Context));
            }
        }
        public IRepository<AutoMaker> AutoMakerRepository
        {
            get
            {
                return this.automakerRepository ?? (this.automakerRepository =
                    new Repository<AutoMaker>(Context));
            }
        }
        public IRepository<AutoModel> AutoModelRepository
        {
            get
            {
                return this.automodelRepository ?? (this.automodelRepository =
                    new Repository<AutoModel>(Context));
            }
        }
        public IRepository<Owner> OwnerRepository
        {
            get
            {
                return this.ownerRepository ?? (ownerRepository =
                    new Repository<Owner>(Context));
            }
        }
        public IRepository<VehicleOwner> VehicleOwnerRepository
        {
            get
            {
                return this.vehicleOwnerRepository ?? (vehicleOwnerRepository =
                    new Repository<VehicleOwner>(Context));
            }
        }
        public IRepository<Vehicle> VehicleRepository
        {
            get
            {
                return this.vehicleRepository ?? (vehicleRepository =
                    new Repository<Vehicle>(Context));
            }
        }
        public void BeginTransaction()
        {
            dbContextTransaction = Context.Database.BeginTransaction(); 
        }
        public Task<int> Commit()
        {
            return Context.SaveChangesAsync();
        }
        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Rollback()
        {
            dbContextTransaction.Rollback();
        }
        public void Save()
        {
            Context.SaveChanges();
        }
        public async Task<Int32> SaveChanges()
        {
            return await Context.SaveChangesAsync();
        }

        public async Task<Int32> SaveChanges(CancellationToken cancellationToken)
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
