const app = Vue.createApp({
    data() {
        return {
            objectName: "Бассейн",
            capacity: 64,
            date_string: "2021-12-21",
            events: [
                {
                    sectionName: "Свободное плавание",
                    time: "12:00",
                    duration: 60,
                    freeSpaces: 63
                }
            ],
        }
    }
});

app.component('date-picker', {
    data() {
        return {
            month_strings: [
                "янв",
                "фев",
                "мар",
                "апр",
                "май",
                "июн",
                "июл",
                "авг",
                "сен",
                "окт",
                "ноя",
                "дек",
            ]
        }
    },
    props: {
      date: String
    },
    computed: {
        day() {
            return (new Date(this.date)).getDate();
        },
        month() {
            return this.month_strings[(new Date(this.date)).getMonth()];
        },
        year() {
            return (new Date(this.date)).getFullYear();
        },
        date() {
            return new Date(this.date);
        }
    },
    template: `
    <div class="date-picker-block">
        <p class="current-date"><span id="day">{{ day }}</span> <span id="month">{{ month }}</span> <span id="year">{{ year }}</span></p>
        <span class="datepicker-toggle">
            <span class="datepicker-toggle-button"></span>
            <input type="date" class="datepicker-input" v-model="date_string">
        </span>
    </div>
    `
});

app.component('page-info', {
    props: ['object_name', 'date'],
    template: `
        <div class="date-block">
            <p class="sport-object-name">{{ object_name }}</p>
            <date-picker :date="date"></date-picker>
        </div>
    `
});

app.component('event-block', {
    props: ['event', 'capacity'],
    computed: {
        value_height() {
            return Math.max((1 - this.event.freeSpaces/this.capacity)*100, 15)+'%';
        },
        value_background() {
            return `hsl(${this.event.freeSpaces/this.capacity*100},85%,65%)`;
        }
    },
    template: `
      <div class="event">
          <div class="capacity-line">
            <div class="value" :style="{ height: value_height, background: value_background}"></div>
<!--            :style="{ height:event.freeSpaces/capacity+'%'}"-->
          </div>
          <div class="info">
            <p class="name">{{ event.sectionName }}</p>
            <p class="time">{{ event.time }}</p>
            <div class="graph-info">
              <div class="duration">
                <img src="/img/clock.svg" alt="">
                <p class="text">{{ event.duration }}м</p>
              </div>
              <div class="capacity-block">
                <div class="diagram">
                  <div class="value"></div>
                </div>
                <p class="count">{{ event.freeSpaces }}</p>
                <p class="text">мест<br/>свободно</p>
              </div>
            </div>
          </div>
          <a href="#" class="sign-up-for-an-event">Записаться</a>
      </div>
    `
});
app.mount("#app");