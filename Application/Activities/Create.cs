using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Domain;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
                    
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                //NOT ACCESSING THE DATABASE AT THIS STAGE THEREFORE NO NEED TO BE ASYNC
                //ADDING ACTIVITY IN MEMORY
                //EF ONLY TRACKING WE ARE ADDING ACTIVITY
                _context.Activities.Add(request.Activity);

                await _context.SaveChangesAsync();

                //REALLY RETURNS NOTHING, JUST A WAY OF LETTING API WE FINISHED ADDING ACTIVITY TO DB
                return Unit.Value;
            }
        }
  }
}