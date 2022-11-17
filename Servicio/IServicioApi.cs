using splash_alert.Models.Servicios;
namespace splash_alert.Servicio
{
    public interface IServicioApi
    {
        Task<bool> ListaAsistente(UsuarioS objeto);
        Task<bool> Listaadmin(UsuarioS objeto);
        Task<bool> GuardarAsistente(UsuarioS objeto);
        Task<bool> GuardarAdmin(UsuarioS objeto);
        Task<bool> GuardarCausaNino(CausaS objeto);
        Task<bool> GuardarCausaAdulto(CausaS objeto);
        Task<bool> GuardarCausaAnimal(CausaS objeto);
        Task<bool> GuardarCausaObjeto(CausaS objeto);



    }
}
