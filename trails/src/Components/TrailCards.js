import TrailCardWrapper from './TrailCardWrapper';
import React from 'react';

export default class TrailCards extends React.Component {
    constructor(props) {
      super(props);
      this.state = {
          fullscreen: false,
          trails: [
            {
                title: 'Mount Bourgeau',
                location: 'Banff, Alberta, Canada',
                images: ['/Bourgeau.jpg', '/Rockwall.jpg', '/Northover.jpg'],
                summary: 'A winding trail leads you up the bourgeau valley up to a serene mountain like. Continue up a steep rocky trail to more mountain lakes. Eventually crest over a hill to reveal vast views of the neighbouring valleys, including a stunning view of Mount Assiniboine. After taking in the vi... <strong>See More...</strong>',
                stats: {
                    distance: 23,
                    elevation: 1600,
                    time: '3-4',
                    season: 'Jun-Sep',
                    rating: 4
                }
            },
            {
                title: 'Rockwall Traverse',
                location: 'Banff, Alberta, Canada',
                images: ['/Rockwall.jpg'],
                summary: 'A winding trail leads you up the bourgeau valley up to a serene mountain like. Continue up a steep rocky trail to more mountain lakes. Eventually crest over a hill to reveal vast views of the neighbouring valleys, including a stunning view of Mount Assiniboine. After taking in the vi... <strong>See More...</strong>',
                stats: {
                    distance: 55,
                    elevation: 2500,
                    time: '6-12',
                    season: 'Jul-Sep',
                    rating: 5
                }
            },
            {
                title: 'Northover Ridge',
                location: 'Kananaskis, Alberta, Canada',
                images: ['/Northover.jpg'],
                summary: 'A winding trail leads you up the bourgeau valley up to a serene mountain like. Continue up a steep rocky trail to more mountain lakes. Eventually crest over a hill to reveal vast views of the neighbouring valleys, including a stunning view of Mount Assiniboine. After taking in the vi... <strong>See More...</strong>',
                stats: {
                    distance: 36,
                    elevation: 1700,
                    time: '4-8',
                    season: 'Jun-Sep',
                    rating: 5
                }
            },
            {
                title: 'Mount Bourgeau',
                location: 'Banff, Alberta, Canada',
                images: ['/Bourgeau.jpg'],
                summary: 'A winding trail leads you up the bourgeau valley up to a serene mountain like. Continue up a steep rocky trail to more mountain lakes. Eventually crest over a hill to reveal vast views of the neighbouring valleys, including a stunning view of Mount Assiniboine. After taking in the vi... <strong>See More...</strong>',
                stats: {
                    distance: 23,
                    elevation: 1600,
                    time: '3-4',
                    season: 'Jun-Sep',
                    rating: 4
                }
            },
          ]
      };
    }

  
    componentDidMount() {
    }
  
    componentWillUnmount() {
    }
  
    render() {
      var trails = this.props.trails.map((t, i) => (<TrailCardWrapper fullscreen={(full) => this.setState({fullscreen: full})} hidden={this.state.fullscreen} trail={t} key={i} />));
      return (
        <div className="card-container">
            {trails}
        </div>
      );
    }
  }