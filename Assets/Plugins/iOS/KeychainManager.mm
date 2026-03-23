#import <Foundation/Foundation.h>
#import <Security/Security.h>

extern "C" {
    // Keychainにデータを保存
    void SaveToKeychain(const char *key, const char *value) {
        NSString *keyString = [NSString stringWithUTF8String:key];
        NSString *valueString = [NSString stringWithUTF8String:value];
        
        NSData *data = [valueString dataUsingEncoding:NSUTF8StringEncoding];
        
        NSDictionary *query = @{
            (__bridge id)kSecClass: (__bridge id)kSecClassGenericPassword,
            (__bridge id)kSecAttrAccount: keyString,
            (__bridge id)kSecValueData: data
        };
        
        SecItemDelete((__bridge CFDictionaryRef)query); // 既存データを削除
        OSStatus status = SecItemAdd((__bridge CFDictionaryRef)query, NULL);
        
        if (status != errSecSuccess) {
            NSLog(@"Error saving to Keychain: %d", (int)status);
        }
    }

    // Keychainからデータを取得
    const char* LoadFromKeychain(const char *key) {
        NSString *keyString = [NSString stringWithUTF8String:key];
        
        NSDictionary *query = @{
            (__bridge id)kSecClass: (__bridge id)kSecClassGenericPassword,
            (__bridge id)kSecAttrAccount: keyString,
            (__bridge id)kSecReturnData: @YES,
            (__bridge id)kSecMatchLimit: (__bridge id)kSecMatchLimitOne
        };
        
        CFDataRef dataRef = NULL;
        OSStatus status = SecItemCopyMatching((__bridge CFDictionaryRef)query, (CFTypeRef *)&dataRef);
        
        if (status == errSecSuccess) {
            NSData *data = (__bridge_transfer NSData *)dataRef; // __bridge_transferで自動的に解放
            NSString *result = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
            return strdup([result UTF8String]);
        } else {
            NSLog(@"Error loading from Keychain: %d", (int)status);
            return NULL;
        }
    }

    // Keychainのデータを削除
    void DeleteFromKeychain(const char *key) {
        NSString *keyString = [NSString stringWithUTF8String:key];
        
        NSDictionary *query = @{
            (__bridge id)kSecClass: (__bridge id)kSecClassGenericPassword,
            (__bridge id)kSecAttrAccount: keyString
        };
        
        OSStatus status = SecItemDelete((__bridge CFDictionaryRef)query);
        
        if (status != errSecSuccess) {
            NSLog(@"Error deleting from Keychain: %d", (int)status);
        }
    }
}
