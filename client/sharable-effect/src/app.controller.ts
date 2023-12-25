import { Controller, Get } from '@nestjs/common';
import { AppService } from './app.service';

@Controller()
export class AppController {
  constructor(private readonly appService: AppService) {}

  @Get('gameList')
  async gameList() {
    const response = await fetch('http://localhost:5000/DocTable/GetXGPTable');
    const data = await response.json();

    return data;
  }
}
