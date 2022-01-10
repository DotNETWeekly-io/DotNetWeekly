import React from 'react';
import { Episode } from './EpisodeData';
import styles from './EpisodeList.module.css';
import { Link } from 'react-router-dom';

interface Props {
    data: Episode[];
    renderItem?: (item: Episode) => JSX.Element;
}

export class EpisodeList extends React.Component<Props> {
    // eslint-disable-next-line @typescript-eslint/no-useless-constructor
    constructor(props: Props) {
        super(props);
    }

    render(): JSX.Element {
        return (
            <ul className={styles.item}>
                {this.props.data.map((episode) => (
                    <li>
                        <Link to={`/episode/${episode.id}`}>
                            {episode.title}
                        </Link>
                    </li>
                ))}
            </ul>
        );
    }
}
