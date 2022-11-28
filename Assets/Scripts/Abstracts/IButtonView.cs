using System;

namespace Abstracts
{
    public interface IButtonView
    {
        public void Init(Action onClickAction);
    }
}