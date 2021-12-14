const app = Vue.createApp({
    components: { 'default-header': header_component },
    data() {
        let params = new URLSearchParams(window.location.search)
        let date = params
        return {
            search_params: params,
            objectName: "Бассейн",
            capacity: 64,
            date_string: date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate(),
            events: [
                {
                    sectionName: "Свободное плавание",
                    time: "12:00",
                    duration: 60,
                    freeSpaces: 80
                }
            ],
        }
    },
    mounted() {
        let month_strings =  [
            "января",
            "февраля",
            "марта",
            "апреля",
            "мая",
            "июня",
            "июля",
            "августа",
            "сентября",
            "октября",
            "ноября",
            "декабря",
        ]
        let date = new Date(this.date_string)
        let options = {
            day: "numeric",
            month: "long",
            year: "numeric"
        }
        document.title = this.objectName + " - " + date.toLocaleDateString("ru", options)
    },
    methods: {
        get_right_form(num, words) {
            if (num % 10 === 0 || num % 10 > 4 || (num % 100 / 10 | 0) === 1)
                return words[0]
            else if (num % 10 === 1)
                return words[1]
            else
                return words[2]
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
                <p class="text">{{ this.$root.get_right_form(event.freeSpaces, ["мест", "место", "места"])}}<br/>свободно</p>
              </div>
            </div>
          </div>
          <a href="#" class="sign-up-for-an-event">Записаться</a>
      </div>
    `
});

app.component('events-block', {
    props: [],
    template: `
        <div class="day-events-container">
            <page-info :object_name="this.$root.objectName" :date="this.$root.date_string"></page-info>
            <div class="events">
              <event-block v-for="event in this.$root.events" :event="event" :capacity="this.$root.capacity"></event-block>
            </div>
            <div class="subscription-info"></div>
        </div>
    `
})
app.mount("#app");