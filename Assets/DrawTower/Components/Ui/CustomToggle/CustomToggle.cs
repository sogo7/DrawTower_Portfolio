using UnityEngine.EventSystems;
using UnityEngine.UI;
using R3;

namespace DrawTower.Ui
{
    public class CustomToggle : Toggle, IPointerDownHandler, IPointerUpHandler
    {
        private readonly Subject<Unit> _onPointerDown = new();
        private readonly Subject<Unit> _onPointerUp = new();

        public Observable<Unit> OnPointerDownAsObservable() => _onPointerDown;
        public Observable<Unit> OnPointerUpAsObservable() => _onPointerUp;

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            _onPointerDown.OnNext(Unit.Default);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            _onPointerUp.OnNext(Unit.Default);
        }

        protected override void OnDestroy()
        {
            _onPointerDown.OnCompleted();
            _onPointerUp.OnCompleted();
            base.OnDestroy();
        }
    }
}