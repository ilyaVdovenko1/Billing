using Billing.Extensions;
using Billing.Interfaces;
using Billing.Models;
using Grpc.Core;

namespace Billing.Services
{
    public class BillingService : Billing.BillingBase
    {
        private readonly ILogger<BillingService> logger;
        private readonly IAppService appService;

        public BillingService(ILogger<BillingService> logger, IAppService appService)
        {
            this.appService = appService;
            this.logger = logger;
        }

        public override async Task ListUsers(None request, IServerStreamWriter<UserProfile> responseStream, ServerCallContext context)
        {
            var users = this.appService.ListUsers().Select(user => user.UserToUserProfile());
            foreach (var user in users)
            {
                await responseStream.WriteAsync(user);
            }
        }
        
        public override Task<Response> CoinsEmission(EmissionAmount emissionAmount, ServerCallContext context)
        {
            try
            {
                this.appService.CoinsEmission(emissionAmount.Amount);

                return Task.FromResult(new Response()
                {
                    Comment = $"Generated {emissionAmount.Amount} coins",
                    Status = Response.Types.Status.Ok,
                });

            }
            catch (ArgumentException e)
            {
                return Task.FromResult(new Response()
                {
                    Comment = e.Message,
                    Status = Response.Types.Status.Failed,
                });
            }
            catch (Exception e)
            {
                return Task.FromResult(new Response()
                {
                    Comment = e.Message,
                    Status = Response.Types.Status.Failed,
                });
            }
        }
        
        public override Task<Response> MoveCoins(MoveCoinsTransaction moveCoinsTransaction, ServerCallContext context)
        {
            try
            {
                var sender = this.appService.FindUserByName(moveCoinsTransaction.SrcUser);
                var recipient = this.appService.FindUserByName(moveCoinsTransaction.DstUser);
                this.appService.MoveCoins(sender, recipient, moveCoinsTransaction.Amount);
                return Task.FromResult(new Response()
                {
                    Comment =
                        $"Moved {moveCoinsTransaction.Amount} coins from {moveCoinsTransaction.SrcUser} to {moveCoinsTransaction.DstUser}",
                    Status = Response.Types.Status.Ok,
                });

            }
            catch (ArgumentException e)
            {
                return Task.FromResult(new Response()
                {
                    Status = Response.Types.Status.Failed,
                    Comment = e.Message
                });
            }
            catch (Exception e)
            {
                return Task.FromResult(new Response()
                {
                    Status = Response.Types.Status.Failed,
                    Comment = e.Message
                });
            }
            
        }

        public override Task<Coin> LongestHistoryCoin(None request, ServerCallContext context)
        {
            var coin = appService.CoinsWithLongestHistory();

            return Task.FromResult(coin.CoinBusinessModelToCoin());
        }
        
    }
}
