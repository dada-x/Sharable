import { HttpService } from '@nestjs/axios';
import { Injectable } from '@nestjs/common';
import { firstValueFrom } from 'rxjs';

@Injectable()
export class AppService {
  constructor(private readonly httpService: HttpService) {}

  async fetchAll(): Promise<[]> {
    const { data } = await firstValueFrom(
      this.httpService.get<[]>('http://localhost:5000/DocTable/GetXGPTable'),
    );

    return data;
  }
}
