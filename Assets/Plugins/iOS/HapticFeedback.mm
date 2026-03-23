#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

extern "C" {
    void PlayHaptic() {
        UIImpactFeedbackGenerator *generator = [[UIImpactFeedbackGenerator alloc] initWithStyle:UIImpactFeedbackStyleMedium];
        [generator prepare];
        [generator impactOccurred];
    }
}
