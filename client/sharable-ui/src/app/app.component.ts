import { Component } from '@angular/core';
import { formatDate } from '@angular/common';
import { ColDef, GridOptions } from 'ag-grid-community';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  $games = this.httpClient.get<[]>('/sharable-ui/api/gameList');

  options: GridOptions = {
    tooltipShowDelay: 1000,
    enableCellTextSelection: true,
    rowHeight: 25,
    defaultColDef: {
      filter: true,
      floatingFilter: true,
      tooltipValueGetter: (params) => {
        return params.value;
      },
    },
  };

  colDefs: ColDef[] = [
    { field: 'enName', headerName: '游戏名', flex: 1 },
    { field: 'cnName', headerName: '游戏名（Steam）', flex: 1 },
    {
      field: 'genre',
      headerName: '游戏类型',
      flex: 1,
      valueFormatter: (params) => {
        return ((params.value as string) ?? '')
          .replace('Action-Adventure', '动作冒险')
          .replace('First-Person Shooter', '第一人称射击')
          .replace('Real-Time Strategy', '即时战略')
          .replace('Simulation', '模拟')
          .replace('Role-Playing', '角色扮演')
          .replace('Soccer', '足球')
          .replace('Driving/Racing', '赛车')
          .replace('Fighting', '格斗')
          .replace('Sports', '体育')
          .replace('Fishing', '钓鱼')
          .replace('Adventure', '冒险')
          .replace('Puzzle', '解谜')
          .replace('Strategy', '策略')
          .replace('Platformer', '平台跳跃')
          .replace('Brawler', '斗殴')
          .replace('Action', '动作')
          .replace('Shooter', '射击')
          .replace('Minigame Collection', '小合集')
          .replace('Music/Rhythm', '音游')
          .replace('Card Game', '卡牌')
          .replace('Horror', '恐怖')
          .replace('Football', '橄榄球')
          .replace('Trivia/Board Game', '下棋')
          .replace('Compilation', '合集')
          .replace('Educational', '教育')
          .replace('Snowboarding/Skiing', '滑板/滑雪')
          .replace('Basketball', '篮球')
          .replace('Skateboarding', '滑板')
          .replace('Flight Simulator', '飞行模拟');
      },
    },
    { field: 'steamPrice', headerName: '$价格（Steam）', width: 150 },
    { field: 'steamDeveloper', headerName: '开发商', flex: 1 },
    {
      field: 'metacritic',
      headerName: 'MC评分',
      type: 'number',
      width: 120,
      cellClass: (params) => {
        if (params.value == '' || params.value == undefined) {
          return '';
        }

        if (Number.parseInt(params.value) > 89) {
          return 'cell-90';
        }

        if (Number.parseInt(params.value) > 79) {
          return 'cell-80';
        }

        if (Number.parseInt(params.value) > 59) {
          return 'cell-60';
        }

        return 'cell-s60';
      },
    },
    { field: 'platform', headerName: '支持平台', width: 120 },
    {
      field: 'status',
      headerName: '状态',
      width: 150,
      cellClass: (params) => {
        if (params.value == 'Active') {
          return 'cell-active';
        }

        return 'cell-soon';
      },
      valueFormatter: (params) => {
        if (params.value == 'Active') {
          return '在库';
        }

        return '即将入库';
      },
    },
    {
      field: 'addTS',
      headerName: '加入时间',
      width: 150,
      type: 'date',
      sort: 'desc',
      valueFormatter: (date) => {
        return formatDate(date.value, 'yyyy-MM-dd', 'en-GB');
      },
    },
    {
      field: 'releaseTS',
      headerName: '发行时间',
      width: 100,
      type: 'date',
      valueFormatter: (date) => {
        return formatDate(date.value, 'yyyy-MM-dd', 'en-GB');
      },
    },
  ];

  constructor(private httpClient: HttpClient) {}
}
