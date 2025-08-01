
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSFIApp.Services.Common
{
    public interface ILoadingTryAgainService
    {
        public interface Partision
        {
            public interface ViewmodelProps : ViewmodelTryAgain
            {
                public bool TurnVisible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

                public void ChnageTurn(bool TurnVisibleValue)
                {
                    throw new NotImplementedException();
                }
            }

            public interface CodeBihindeProps : CodeBihindeTryAgain
            {
                public void OnTryAgainClick(object sender, EventArgs e)
                {
                    throw new NotImplementedException();
                }
            }
        }


        public interface All_In : BothIn_OnePlace
        {

        }
    }

    public interface ViewmodelTryAgain
    {
        public bool TurnVisible { get; set; }
 
        public void ChnageTurn(bool TurnVisibleValue);
    }
    public interface CodeBihindeTryAgain
    {
        public void OnTryAgainClick(object sender, EventArgs e);
    }

    public interface BothIn_OnePlace
    {
        public bool TurnVisible { get; set; }
        public void ChnageTurn(bool TurnVisibleValue);
        public void OnTryAgainClick(object sender, EventArgs e);
    }

 

}
