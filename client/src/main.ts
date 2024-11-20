import { bootstrapApplication } from "@angular/platform-browser";
import { appConfig } from "./app/app.config";
import { AppComponent } from "./app/app.component";

bootstrapApplication(AppComponent, appConfig).catch((err) =>
  console.error(err)
);

//To set global config that applies on all the galleries across the app, provide the config value using GALLERY_CONFIG token.
// import { bootstrapApplication } from "@angular/platform-browser";
// import { appConfig } from "./app/app.config"; // Ensure this file exports the configuration
// import { AppComponent } from "./app/app.component";
// import { GALLERY_CONFIG, GalleryConfig } from "ng-gallery";

// bootstrapApplication(AppComponent, {
//   ...appConfig, // Spread the appConfig properties here
//   providers: [
//     ...appConfig.providers, // Include existing providers from appConfig
//     {
//       provide: GALLERY_CONFIG,
//       useValue: {
//         autoHeight: true,
//         imageSize: "cover",
//       } as GalleryConfig,
//     },
//   ],
// }).catch((err) => console.error(err));
