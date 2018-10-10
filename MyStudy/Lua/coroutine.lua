
    
    -- coroutine_test.lua 文件
    function foo(a)
        print("foo 函数输出", a)
        -- 重点在这里 ********************
        return coroutine.yield(999) -- 此时的999是一个参数， 作为第一个resume的返回值， 同时，yield方法的返回值是谁呢，是第二个resume方法的参数， 记住是第二个resume
    end

    co2 =
        coroutine.create(
        function(a)
            print(coroutine.status(co))
            print("我是新携程 1" .. a) -- 当协程开启另外一个协程的时候，另外一个携程， 执行完的时候， 或者遇到yield的时候， 就会返回给原协程
            coroutine.yield() -- 当被调用的新携程， 遇到yield的时候， 就回到了原来的携程
            print("我是新携程 2" .. a)
        end
    )

    co =
        coroutine.create(
        function(a, b)
            print("第一次协同程序执行输出,我马上开启另外一个携程", a, b) -- co-body 1 10
            coroutine.resume(co2, 9) 
            print("第一次协同回来了") -- co-body 1 10

            local r = foo(a + 1)
            print("r=" .. r)

            print("第二次协同程序执行输出", r)
            local r, s = coroutine.yield(a + b, a - b) -- a，b的值为第一次调用协同程序时传入, 当下个携程开启的时候， 就是我得到返回值的时候
            print("r and s is :" ,r,s )
            print("第三次协同程序执行输出", r, s)
            return b, "结束协同程序" -- b的值为第二次调用协同程序时传入
        end
    )
    -- 首先要明白协程是co , 接收两个参数
    -- 开启这个携程的时候， 遇到， yield的时候， 主线程就返回， 此时，返回的就是， yield的参数， 此时协程在挂起的状态
    --
    print("main", coroutine.resume(co, 1, 9998)) -- true, 4   -- 我开启以后，如果我遇到了yield函数，那么他的参数， 就会作为我的返回值
    print("--分割线----")
    print("main", coroutine.resume(co, "我是第一个yield的返回值")) -- true 11 -9
    print("---分割线---")
    print("main", coroutine.resume(co, "x")) -- true 10 end -- 最后一个resume的返回值， 就是主函数的返回值
    print("---分割线---")
    print("main", coroutine.resume(co, "x", "y")) -- cannot resume dead coroutine 没有四个协程， 此时已经死了
    print("---分割线---")

 
