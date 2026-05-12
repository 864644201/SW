using System;
using System.Collections.Generic;

namespace MD3DToolsData
{
    /// <summary>
    /// 模块间数据通信桥接类。
    /// 提供线程安全的键值对存储，用于在不同工具模块之间共享数据。
    /// </summary>
    public static class ToolsData
    {
        private static readonly Dictionary<string, object> _dataStore = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        private static readonly object _syncRoot = new object();

        /// <summary>
        /// 存储数据
        /// </summary>
        /// <param name="key">数据键名（不区分大小写）</param>
        /// <param name="value">数据值，可为任意类型</param>
        /// <exception cref="ArgumentNullException">key 为 null 时抛出</exception>
        public static void SetData(string key, object value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            lock (_syncRoot)
            {
                _dataStore[key] = value;
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key">数据键名（不区分大小写）</param>
        /// <returns>存储的数据值，键不存在返回 default(T)</returns>
        public static T GetData<T>(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            lock (_syncRoot)
            {
                if (_dataStore.TryGetValue(key, out object value))
                {
                    if (value is T typedValue)
                        return typedValue;

                    // 尝试进行类型转换
                    try
                    {
                        return (T)Convert.ChangeType(value, typeof(T));
                    }
                    catch
                    {
                        return default(T);
                    }
                }
            }

            return default(T);
        }

        /// <summary>
        /// 获取数据（返回 object 类型）
        /// </summary>
        /// <param name="key">数据键名（不区分大小写）</param>
        /// <returns>存储的数据值，键不存在返回 null</returns>
        public static object GetData(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            lock (_syncRoot)
            {
                _dataStore.TryGetValue(key, out object value);
                return value;
            }
        }

        /// <summary>
        /// 尝试获取数据
        /// </summary>
        /// <param name="key">数据键名（不区分大小写）</param>
        /// <param name="value">输出参数，获取到的数据值</param>
        /// <returns>键存在返回 true，否则返回 false</returns>
        public static bool TryGetData<T>(string key, out T value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            lock (_syncRoot)
            {
                if (_dataStore.TryGetValue(key, out object objValue))
                {
                    if (objValue is T typedValue)
                    {
                        value = typedValue;
                        return true;
                    }

                    try
                    {
                        value = (T)Convert.ChangeType(objValue, typeof(T));
                        return true;
                    }
                    catch
                    {
                        value = default(T);
                        return false;
                    }
                }
            }

            value = default(T);
            return false;
        }

        /// <summary>
        /// 检查指定键是否存在
        /// </summary>
        /// <param name="key">数据键名（不区分大小写）</param>
        /// <returns>键存在返回 true，否则返回 false</returns>
        public static bool HasData(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            lock (_syncRoot)
            {
                return _dataStore.ContainsKey(key);
            }
        }

        /// <summary>
        /// 移除指定键的数据
        /// </summary>
        /// <param name="key">数据键名（不区分大小写）</param>
        /// <returns>成功移除返回 true，键不存在返回 false</returns>
        public static bool RemoveData(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            lock (_syncRoot)
            {
                return _dataStore.Remove(key);
            }
        }

        /// <summary>
        /// 清除所有存储的数据
        /// </summary>
        public static void ClearData()
        {
            lock (_syncRoot)
            {
                _dataStore.Clear();
            }
        }

        /// <summary>
        /// 获取当前存储的数据项数量
        /// </summary>
        public static int Count
        {
            get
            {
                lock (_syncRoot)
                {
                    return _dataStore.Count;
                }
            }
        }

        /// <summary>
        /// 获取所有已存储的键名
        /// </summary>
        /// <returns>键名集合的副本</returns>
        public static ICollection<string> GetKeys()
        {
            lock (_syncRoot)
            {
                return new List<string>(_dataStore.Keys);
            }
        }
    }
}
