using Microsoft.EntityFrameworkCore;
using StartingTask_ITV.Core.Interfaces;
using StartingTask_ITV.Core.Model;

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

        public async Task AddDeviceAsync(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type), "тип оборудования не может быть пустым");

            await sqlite.Devices.AddAsync(new Model.Device { Type = type });
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
