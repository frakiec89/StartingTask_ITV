using StartingTask_ITV.Core.Model;

namespace StartingTask_ITV.Core.Interfaces
{
    public interface IDeviceService
    {

        /// <summary>
        /// Добавить новое устройство
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        Task AddDeviceAsync(int  id ,  string type);

        /// <summary>
        /// Удаление устройсва по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">Не найден</exception>
        Task DeleteDeviceAsync(int id);

        /// <summary>
        /// Удаление вcех устройcтв по типу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">Не найден</exception>
        Task DeleteDevicesTypeAsync(string type);
      
        Task<IEnumerable<Device>> GetDevicesAsync(); // todo добавить перегрузку для поиска по диапазону
    }
}
