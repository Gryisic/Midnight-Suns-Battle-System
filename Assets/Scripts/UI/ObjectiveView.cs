namespace UI
{
    public class ObjectiveView : UIElement
    {
        public override void Activate() => gameObject.SetActive(true);

        public override void Deactivate() => gameObject.SetActive(false);
    }
}