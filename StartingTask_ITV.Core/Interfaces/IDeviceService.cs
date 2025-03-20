using StartingTask_ITV.Core.Model;

namespace StartingTask_ITV.Core.Interfaces
{
    public interface IDeviceService
    {
        Task AddDeviceAsync(string type);

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
