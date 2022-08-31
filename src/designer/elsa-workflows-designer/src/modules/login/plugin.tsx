import 'reflect-metadata';
import {h} from "@stencil/core";
import {Service as MiddlewareService} from 'axios-middleware';
import {EventTypes, Plugin} from "../../models";
import {Container, Service} from "typedi";
import {StudioService, AuthContext, EventBus} from "../../services";

@Service()
export class LoginPlugin implements Plugin {
  private eventBus: EventBus;
  private studioService: StudioService;

  constructor() {
    this.eventBus = Container.get(EventBus);
    this.studioService = Container.get(StudioService);
    this.eventBus.on(EventTypes.HttpClient.ClientCreated, this.onHttpClientCreated);
    this.eventBus.on(EventTypes.HttpClient.Unauthorized, this.onUnauthorized)
  }

  async initialize(): Promise<void> {
    const authContext = Container.get(AuthContext);

    if (!authContext.getIsSignedIn()) {
      this.studioService.show(() => <elsa-login-page/>);
    }
  }

  private onHttpClientCreated = async (e) => {
    const service: MiddlewareService = e.service;

    service.register({
      async onRequest(request) {
        const authContext = Container.get(AuthContext);
        const token = authContext.getAccessToken();

        if (!!token)
          request.headers = {...request.headers, 'Authorization': `Bearer ${token}`};

        return request;
      }
    });
  };

  private onUnauthorized = async () => {
    this.studioService.show(() => <elsa-login-page/>);
  };

}
