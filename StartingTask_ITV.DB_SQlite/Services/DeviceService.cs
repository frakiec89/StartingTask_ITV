using Microsoft.EntityFrameworkCore;
using StartingTask_ITV.Core.Interfaces;

namespace StartingTask_ITV.DB_SQlite.Services
{
    public class DeviceService : IDeviceService
    {
        private SqliteContextEF sqlite;

        public DeviceService()
        {
            sqlite = new SqliteContextEF(); // todo зависимость         
        }

        public async Task<IEnumerable<Core.Model.Device>> GetDevicesAsync()
        {
            var devicesDb = await sqlite.Devices.ToListAsync();
            return devicesDb.Select(device => device.GetCoreDevice()).ToList();
        }

        /// <summary>
        /// Добавить новое устройство
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task AddDeviceAsync(int id ,  string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "тип оборудования не может быть пустым");


            if (await sqlite.Devices.AnyAsync(x=>x.Id ==id))
                throw new InvalidOperationException("Устройство с таким Id уже существует.");

            await sqlite.Devices.AddAsync(new Model.Device { Id=id ,  Type = type });
            await sqlite.SaveChangesAsync();
        }

        /// <summary>
        /// Удаление устройсва по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">Не найден</exception>
        public async Task DeleteDeviceAsync(int id)
        {
            if (sqlite.Devices.Any(device => device.Id == id) == false)
                throw new NotImplementedException();

            var device = await sqlite.Devices.FindAsync(id);

            if (device != null)
            {
                sqlite.Devices.Remove(device);
                await sqlite.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Удаление вcех устройcтв по типу
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">Не найден</exception>
        public async Task DeleteDevicesTypeAsync(string type)
        {
            if (sqlite.Devices.Any(device => device.Type == type) == false)
                throw new NotImplementedException();

            var devices = sqlite.Devices.Where(x => x.Type == type);

            sqlite.Devices.RemoveRange(devices);
            await sqlite.SaveChangesAsync();
        }
    }
}
