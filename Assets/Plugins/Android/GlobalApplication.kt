package com.DefaultCompany.BehindSeoul

import android.app.Application
import com.kakao.sdk.common.KakaoSdk
import com.kakao.sdk.common.util.Utility

class GlobalApplication : Application() {
    override fun onCreate() {
        super.onCreate()
        // 다른 초기화 코드들

        // Kakao SDK 초기화
        KakaoSdk.init(this, "1c96c6697328d426ad301842bac41f7a")

        val keyHash = Utility.getKeyHash(this)
        println("unitylog : $$keyHash")
    }
}