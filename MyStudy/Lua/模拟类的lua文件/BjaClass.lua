
---
--- 说明：类模块，用以实现面向对象编程的类
--- 注意：运算符重载无法继承

BjaClass = {}

-- 类生成器（base：基类，无则忽略）
BjaClass.MakeClass = function(base)

    -- 创建类表
    local class = {}
    -- （1）此步骤当Class作为元表时，能够基表访问到（否则静态属性无法访问）；
    class.__index = class
    -- （2）此步骤使通过object更改Class的静态变量时，能够作用在Class上（否则只能通过Class.静态名 = xxx 更改静态变量）；
    class.__newindex = function(object,k,v)
        -- 这么处理是避免object添加非静态变量时直接作用在Class表上；
        if class[k] then    -- 当Class上存在k键时，说明这个是静态变量；
            class[k] = v
        else                -- 否则，则操作对象obj自身的成员；
            rawset(object,k,v)
        end
    end
    -- （3）创建类的私有静态表（访问类的私有静态时，可以是 "Class.privateStatic.静态名" 的形式）
    do
        local privateStatic = {}
        -- 元表索引向基类的private静态表，索引更新也指向基类的private静态表（避免更新数据时，重复创建private数据）
        setmetatable(privateStatic,{__index = class.privateStatic,__newindex = class.privateStatic})
        class.privateStatic = privateStatic
    end


    -- 创建类元表
    local class_mt = {}
    -- （1）此处元表作用：1.继承；2.创建Class()形式的构造器
    setmetatable(class,class_mt)
    -- （2）继承
    class_mt.__index = base or nil

    return class
end
-- 构造器生成器（class：构造器的类；CtorFunc：构造器函数，满足func(class,...)格式，至少要1个class参数）
BjaClass.MakeCtor = function(class,CtorFunc)
    -- 获取类的元表
    class_mt = getmetatable(class)
    -- 利用了元方法，以实现Class()格式即可创建对象
    class_mt.__call = CtorFunc
end
-- 类对象生成器（class：所要生成对象的类；...：有基类时，则用以传入基类，无基类时，用以初始化本类的公有变量）
BjaClass.MakeObj = function(class,...)

    -- 获取基类
    local mt = getmetatable(class)
    local base = mt.__index
    -- 创建Class对象
    local obj = {}
    if base then    -- 存在基类则调用base来生成对象
        obj = base(...)
    else            -- 不存在，则参数用来初始化对象的公有成员
        for key,val in pairs({...})do
            obj[key] = val
        end
    end
    -- 创建对象的私有表（每个对象都有1个）
    do
        local private = {}
        -- 元表索引向基类对象的private表，索引更新也指向基类对象的private表（避免更新数据时，重复创建private数据）
        setmetatable(private,{__index = obj.private,__newindex = obj.private})
        -- 另对象的私有表从"基类对象私有表"指向"本类对象私有表"
        obj.private = private
    end

    -- 使对象与类关联
    setmetatable(obj,class)

    return obj
end

return BjaClass