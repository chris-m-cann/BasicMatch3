
namespace Util
{
    public interface GameEventListener<T>
    {
        void OnEventRaised(T t);
    }
}