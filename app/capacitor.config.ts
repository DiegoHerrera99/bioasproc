import type { CapacitorConfig } from '@capacitor/cli';

const config: CapacitorConfig = {
  appId: 'com.bioasproc.app',
  appName: 'Bioasproc',
  webDir: 'www'
,
    android: {
       buildOptions: {
          keystorePath: '/Users/diegoherrera/Documents/firma-android-apk (original)',
          keystoreAlias: 'diegoherrera.tech',
       }
    }
  };

export default config;
