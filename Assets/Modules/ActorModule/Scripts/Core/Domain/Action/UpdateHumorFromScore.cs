using Modules.Common;

namespace Modules.ActorModule.Scripts.Core.Domain.Action
{
    public class UpdateHumorFromScore
    {
        private SessionRepository sessionRepository;
        private UpdateHumor updateHumor;
        private HumorStateService humorStateService;
        public UpdateHumorFromScore(SessionRepository sessionRepository,
            UpdateHumor updateHumor,
            HumorStateService humorStateService)
        {
            this.sessionRepository = sessionRepository;
            this.updateHumor = updateHumor;
            this.humorStateService = humorStateService;
        }
        public void Execute(float newScore)
        {
            sessionRepository.Get().Do(session =>
            {
                updateHumor.Execute(humorStateService.ProcessPointsFromGames(newScore), session.actorId);
            });
        }
    }
}