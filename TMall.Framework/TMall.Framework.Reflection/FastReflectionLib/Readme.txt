var propertyInfo = obj.GetType().GetProperty("ObjectProperty");

//构造好的表达式树操作类
var accessor = FastReflectionCaches.PropertyAccessorCache.Get(propertyInfo);

//扩展方法使用
 var value = propertyInfo.FastGetValue(obj);