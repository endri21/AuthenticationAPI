/*
 Per te mos bere qe ne cdo metode te kontrollojme eshte null ose jo 
krijojme nje klase statike qe kthen ose notfound ose kthen vete modelin 




ne kete projekt nuk eshte perdorur ky model 
 */


using Microsoft.AspNetCore.Mvc;
namespace CoreApiRegister.Infrastructure.Extensions
{
    public static class ObjectsExtensions
    {
        public static ActionResult<TModel> OrNotFound<TModel>(this TModel model)
        {
            if(model == null)
            {
                return new NotFoundResult();
            }
            return model;
        }
    }
}
